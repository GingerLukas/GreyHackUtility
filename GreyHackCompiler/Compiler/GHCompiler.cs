﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using GreyHackUtils.Helpers;
using Newtonsoft.Json;

namespace GreyHackCompiler
{
    class GHCompiler
    {
        public static GHCompiler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GHCompiler();
                }

                return _instance;
            }
        }

        private static GHCompiler _instance;


        private Dictionary<string, string> _pairs;

        private HashSet<string> _keywords;
        private HashSet<char> _operators;
        private HashSet<char> _no_space;
        private HashSet<string> _included;

        private List<string> _out_list;
        private List<char> _tmp_value;
        private List<int> _index;
        private List<char> _abc;
        private List<char> _next_out;

        private Stack<bool> _map_active;
        private Stack<bool> _value_active;

        private Queue<char> _queue;

        private string _tmp_value_string;
        private string _tmp_out;
        public string PathToLocalClasses
        {
            get => _path_to_classes;
            set => _path_to_classes = value;
        }
        private string _path_to_classes = String.Empty;

        private string _config_path = "config.json";

        private List<string> _errors_include;
        private List<string> _errors_compile;

        public long LastTimeTicks = 0;

        public double BeforeLength = 0;
        public double AfterLength = 0;

        private Stopwatch sw = new Stopwatch();

        public CommentSettings Comment = CommentSettings.NONE;

        public GHCompiler()
        {
            
            _pairs = new Dictionary<string, string>();
            _keywords = new HashSet<string>();
            _operators = new HashSet<char>();
            _no_space = new HashSet<char>();
            _included = new HashSet<string>();
            _out_list = new List<string>();
            _tmp_value = new List<char>();
            _index = new List<int>();
            _abc = new List<char>();
            _next_out = new List<char>();
            _errors_include = new List<string>();
            _errors_compile = new List<string>();
            _map_active = new Stack<bool>();
            _value_active = new Stack<bool>();
            _index.Add(-1);

            Setup();
        }

        #region Internal

        private void Setup()
        {
            //keywords init
            string tmp =
                "clear_screen __isa classID hasIndex for end in abs print range if function not while then else and or true false null return continue break function new self typeof md get_router get_shell nslookup whois is_valid_ip is_lan_ip command_info current_date parent_path home_dir program_path active_user user_mail_address user_bank_number format_columns user_input include_lib exit public_ip local_ip computer_ports computers_lan_ip ping_port port_info used_ports bssid_name essid_name change_password create_user create_group create_folder close_program connect_wifi delete_user delete_group groups network_devices get_ports is_network_active lan_ip show_procs current_path touch wifi_networks File copy move rename chmod set_content set_group group path content is_binary is_folder has_permission owner permissions parent name size delete get_folders get_files get_lan_ip is_closed port_number connect_service scp launch build start_terminal put host_computer aircrack airmon decipher smtp_user_list overflow lib_name version load net_use scan scan_address dump_lib device_ports devices_lan_ip lastIndexOf split replace trim code lower upper val to_int abs acos asin atan tan cos sin char floor round rnd sign sqrt str ceil pi slice join pull reverse sort hasIndex indexOf push remove indexes len pop shuffle sum values time params globals locals";
            foreach (string s in tmp.Split(' '))
            {
                _keywords.Add(s);
            }

            //operators init
            tmp = "< > ! = + * - / .";
            foreach (string s in tmp.Split(' '))
            {
                _operators.Add(s[0]);
            }

            tmp += "( ) , :";
            foreach (string s in tmp.Split(' '))
            {
                _no_space.Add(s[0]);
            }

            //abc init
            for (int i = 'a'; i <= 'z'; i++)
            {
                _abc.Add((char)i);
            }
            for (int i = 'A'; i <= 'Z'; i++)
            {
                _abc.Add((char)i);
            }
            _value_active.Push(false);
            _map_active.Push(false);
        }

        public void Save()
        {
            Dictionary<string, object> save = new Dictionary<string, object>();

            //adding to save
            save[nameof(_path_to_classes)] = _path_to_classes;


            //writing to file
            string json = JsonConvert.SerializeObject(save);
            File.WriteAllText(_config_path, json);
        }
        
        public void Load()
        {
            if (!File.Exists(_config_path))
            {
                Save();
                return;
            }

            Dictionary<string, object> save =
                JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText(_config_path));


            var s = (string)save[nameof(_path_to_classes)];
            if (File.Exists(s))
            {
                _path_to_classes = s;
            }

        }

        public string Next()
        {
            //clear prev out
            _next_out.Clear();
            for (int i = 0; i < _index.Count; i++)
            {
                _index[i]++;
                if (_index[i] >= _abc.Count)
                {
                    _index[i] = 0;
                    if (i + 1 == _index.Count)
                    {
                        _index.Add(-1);
                    }
                }
                else
                {
                    break;
                }
            }

            //build new out
            foreach (int i in _index)
            {
                _next_out.Add(_abc[i]);
            }
            string tmp = new string(_next_out.ToArray());
            if (_keywords.Contains(tmp))
            {
                return Next();
            }

            return tmp;
        }

        private void ResetInclude()
        {
            //errors reset
            _errors_include.Clear();

            //included classes reset
            _included.Clear();
        }

        private void ResetCompiler()
        {
            //errors reset
            _errors_compile.Clear();

            //pairs generator reset
            _index.Clear();
            _index.Add(-1);
            _pairs.Clear();

            //output reset
            _out_list.Clear();

            //stacks reset
            _value_active.Clear();
            _value_active.Push(false);
            _map_active.Clear();
            _map_active.Push(false);
        }

        private void RemoveWhiteSpaces(bool lines = false)
        {
            if (lines)
            {
                while (_queue.Count > 0 && char.IsWhiteSpace(_queue.Peek()))
                {
                    _queue.Dequeue();
                }
            }
            else
            {
                while (_queue.Count > 0 && _queue.Peek() == ' ')
                {
                    _queue.Dequeue();
                }
            }

        }


        #endregion

        #region Include

        public string GetClassFromGitHub(string path)
        {
            try
            {
                string raw_url = @"https://raw.githubusercontent.com";
                string url = $"{raw_url}/{path}";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        return "";
                    }
                    using (Stream stream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }

                }
            }
            catch (Exception e)
            {
                _errors_include.Add($"ERROR::GITHUB_CLASS_NOT_FOUND({path})");
                return "";
            }


        }

        public string GetClassFromFile(string name)
        {
            string path = Path.Combine(_path_to_classes, name);
            if (!File.Exists(path))
            {
                _errors_include.Add($"ERROR::LOCAL_CLASS_NOT_FOUND({name})");
                return "";
            }

            return File.ReadAllText(path);
        }

        public string GetClass(string name)
        {
            if (name.Length > 3 && name.Substring(0, 3) == "git")
            {
                return GetClassFromGitHub(name.Substring(4));
            }

            return GetClassFromFile(name);
        }

        public string Include(string input, string start = "#!", string end = "!", bool first = true, bool start_sw = true)
        {
            if (first)
            {
                if (start_sw)
                    sw.Restart();

                ResetInclude();
                BeforeLength = input.Length;
            }
            StringBuilder sb = new StringBuilder();
            StringBuilder class_sb = new StringBuilder();
            int index = 0;
            while (index < input.Length)
            {
                if (Helper.CompareStrings(input, index, start))
                {
                    class_sb.Clear();
                    index += start.Length;
                    while (!Helper.CompareStrings(input, index, end))
                    {
                        class_sb.Append(input[index]);
                        index++;
                    }

                    index += end.Length;
                    string class_tmp = class_sb.ToString();


                    if (!_included.Contains(class_tmp))
                    {
                        _included.Add(class_tmp);

                        sb.Append(Include(GetClass(class_tmp), start, end, false, start_sw));
                    }
                }
                else
                {
                    sb.Append(input[index]);
                    index++;
                }
            }

            if (first)
            {
                if (start_sw)
                {
                    sw.Stop();
                    LastTimeTicks = sw.ElapsedTicks;
                }

                AfterLength = sb.Length;

                if (_errors_include.Count != 0)
                {
                    StringBuilder error_sb = new StringBuilder();
                    foreach (string s in _errors_include)
                    {
                        error_sb.AppendLine(s);
                    }

                    MessageBox.Show(error_sb.ToString(),
                        _errors_include.Count + (_errors_include.Count == 1 ? "Error" : "Errors"));
                }
            }


            return sb.ToString();
        }

        #endregion



        public string Compile(string input)
        {
            ResetCompiler();
            _queue = new Queue<char>(input);





            _tmp_out = string.Join("", _out_list);
            sw.Stop();
            LastTimeTicks = sw.ElapsedTicks;
            AfterLength = _tmp_out.Length;
            return _tmp_out;
        }

        public string Optimize(string input, string start = "#!", string end = "!")
        {

            //compiler reset
            ResetCompiler();
            sw.Restart();
            input = Include(input, start, end, true, false);
            //input string to queue
            BeforeLength = input.Length;
            _queue = new Queue<char>(input);

            while (_queue.Count > 0)
            {
                //updates next value
                //I know its ineffective but it sooo much easier
                _tmp_value.Clear();
                _tmp_value.Add(_queue.Dequeue());
                if (_no_space.Contains(_tmp_value[0]))
                {
                    while (_out_list.Last()[0] == ' ')
                    {
                        _out_list.RemoveAt(_out_list.Count - 1);
                    }


                    RemoveWhiteSpaces();
                }

                if (char.IsLetter(_tmp_value[0]) || _tmp_value[0] == '_')
                {
                    while (_queue.Count > 0 && (char.IsLetter(_queue.Peek()) || _queue.Peek() == '_'))
                    {
                        _tmp_value.Add(_queue.Dequeue());
                    }

                    _tmp_value_string = new string(_tmp_value.ToArray());
                    if (!_keywords.Contains(_tmp_value_string))
                    {
                        if (!_pairs.ContainsKey(_tmp_value_string))
                        {
                            _pairs.Add(_tmp_value_string, Next());
                        }

                        _tmp_value_string = _pairs[_tmp_value_string];
                    }
                }
                else
                {
                    switch (_tmp_value[0])
                    {
                        case '\n':
                            RemoveWhiteSpaces(true);
                            _tmp_value_string = "\n";
                            break;
                        case ' ':
                            while (_queue.Count > 0 && _queue.Peek() == ' ')
                            {
                                _queue.Dequeue();
                            }
                            _tmp_value_string = " ";
                            break;
                        case '(':
                            _value_active.Push(false);
                            _tmp_value_string = "(";
                            break;
                        case ')':
                            _value_active.Pop();
                            _tmp_value_string = ")";
                            break;
                        case '"':
                            while (_queue.Count > 0 && _queue.Peek() != '"')
                            {
                                _tmp_value.Add(_queue.Dequeue());
                            }

                            if (_queue.Count > 0)
                            {
                                _tmp_value.Add(_queue.Dequeue());
                            }

                            if (_value_active.Peek())
                            {
                                RemoveWhiteSpaces();
                                
                                if (_tmp_value.Last() == '"')
                                {
                                    _tmp_value.RemoveAt(_tmp_value.Count - 1);
                                }
                                if (_tmp_value.Count > 0 && _tmp_value[0] == '"')
                                {
                                    _tmp_value.RemoveAt(0);
                                }

                                _tmp_value_string = new string(_tmp_value.ToArray());
                                if (_keywords.Contains(_tmp_value_string))
                                {
                                    _tmp_value_string = "\"" + _tmp_value_string + "\"";
                                    break;
                                }

                                if (!_pairs.ContainsKey(_tmp_value_string))
                                {
                                    _pairs.Add(_tmp_value_string, Next());
                                }

                                _tmp_value_string = "\"" + _pairs[_tmp_value_string] + "\"";
                                break;
                                
                            }
                            _tmp_value_string = new string(_tmp_value.ToArray());

                            break;
                        case '{':
                            while (_queue.Peek() == ' ')
                            {
                                _queue.Dequeue();
                            }

                            if (_queue.Peek() != '}')
                            {
                                _map_active.Push(true);
                                _value_active.Push(true);
                            }

                            _tmp_value_string = "{";
                            break;
                        case '}':
                            if (_out_list.Last() != "{")
                            {
                                _map_active.Pop();
                            }

                            _tmp_value_string = "}";
                            break;
                        case '[':
                            while (_queue.Peek() == ' ')
                            {
                                _queue.Dequeue();
                            }

                            if (_queue.Peek() != ']')
                            {
                                if (_operators.Contains(_out_list.Last()[0]))
                                {
                                    _value_active.Push(false);
                                }
                                else
                                {
                                    _value_active.Push(true);
                                }
                                _map_active.Push(false);
                            }

                            _tmp_value_string = "[";
                            break;
                        case ']':
                            if (_out_list.Last() != "[")
                            {
                                _value_active.Pop();
                                _map_active.Pop();
                            }

                            _tmp_value_string = "]";
                            break;
                        case ',':
                            if (_map_active.Peek())
                            {
                                _value_active.Push(true);
                            }

                            _tmp_value_string = ",";
                            break;
                        case ':':
                            if (_map_active.Peek())
                            {
                                _value_active.Pop();
                            }

                            _tmp_value_string = ":";
                            break;
                        default:
                            _tmp_value_string = _tmp_value[0].ToString();
                            break;
                    }
                }

                _out_list.Add(_tmp_value_string);
            }

            _tmp_out = string.Join("", _out_list);
            //_tmp_out = _tmp_out.Replace('\n', ';');
            sw.Stop();
            LastTimeTicks = sw.ElapsedTicks;
            AfterLength = _tmp_out.Length;
            return _tmp_out;

        }
    }

    public enum CommentSettings
    {
        NONE = 0,
        REMOVE = 1,
        SHORTEN = 2,
        KEEP = 4
    }
}
