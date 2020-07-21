using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreyHackCompiler
{
    class GHCompiler
    {
        private Dictionary<string, string> _pairs;
        private HashSet<string> _keywords;
        private HashSet<char> _operators;
        private HashSet<char> _no_space;
        private List<string> _out_list;
        private Stack<bool> _map_active;
        private Stack<bool> _value_active;
        private List<char> _tmp_value;
        private string _tmp_value_string;
        private Queue<char> _queue;
        private List<int> _index;
        private List<char> _abc;
        private List<char> _next_out;
        public long LastOptimizeTimeTicks = 0;
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
            _out_list = new List<string>();
            _map_active = new Stack<bool>();
            _value_active = new Stack<bool>();
            _tmp_value = new List<char>();
            _index = new List<int>();
            _index.Add(-1);
            _abc = new List<char>();
            _next_out = new List<char>();

            Setup();
        }

        private void Setup()
        {
            //keywords init
            string tmp =
                "classID hasIndex for end in abs print range if function not while then else and or true false null return continue break function new self typeof md get_router get_shell nslookup whois is_valid_ip is_lan_ip command_info current_date parent_path home_dir program_path active_user user_mail_address user_bank_number format_columns user_input include_lib exit public_ip local_ip computer_ports computers_lan_ip ping_port port_info used_ports bssid_name essid_name change_password create_user create_group create_folder close_program connect_wifi delete_user delete_group groups network_devices get_ports is_network_active lan_ip show_procs current_path touch wifi_networks File copy move rename chmod set_content set_group group path content is_binary is_folder has_permission owner permissions parent name size delete get_folders get_files get_lan_ip is_closed port_number connect_service scp_upload launch build start_terminal put host_computer aircrack airmon decipher smtp_user_list overflow lib_name version load net_use scan scan_address dump_lib device_ports devices_lan_ip lastIndexOf split replace trim code lower upper val to_int abs acos asin atan tan cos sin char floor round rnd sign sqrt str ceil pi slice join pull reverse sort hasIndex indexOf push remove indexes len pop shuffle sum values time params globals locals";
            foreach (string s in tmp.Split(' '))
            {
                _keywords.Add(s);
            }

            //operators init
            tmp = "< > ! = + * - / :";
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

        public string Next()
        {
            //clear prev out
            _next_out.Clear();
            for (int i = 0; i < _index.Count; i++)
            {
                _index[i]++;
                if (_index[i]>=_abc.Count)
                {
                    _index[i] = 0;
                    if (i+1==_index.Count)
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

        private void Reset()
        {
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

        public string Optimize(string input)
        {
            //compiler reset
            Reset();
            sw.Reset();
            sw.Start();

            //input string to queue
            BeforeLength = input.Length;
            _queue = new Queue<char>(input);
            

            while (_queue.Count > 0)
            {
                //updates next value
                _tmp_value.Clear();
                _tmp_value.Add(_queue.Dequeue());
                if (_no_space.Contains(_tmp_value[0]))
                {
                    while (_out_list.Last()[0]==' ')
                    {
                        _out_list.RemoveAt(_out_list.Count-1);
                    }

                    while (_queue.Count > 0 && _queue.Peek()==' ')
                    {
                        _queue.Dequeue();
                    }
                }

                if (char.IsLetter(_tmp_value[0])||_tmp_value[0] =='_')
                {
                    while (_queue.Count>0 && (char.IsLetter(_queue.Peek())||_queue.Peek()=='_'))
                    {
                        _tmp_value.Add(_queue.Dequeue());
                    }

                    _tmp_value_string = new string(_tmp_value.ToArray());
                    if (!_keywords.Contains(_tmp_value_string))
                    {
                        if (!_pairs.ContainsKey(_tmp_value_string))
                        {
                            _pairs.Add(_tmp_value_string,Next());
                        }

                        _tmp_value_string = _pairs[_tmp_value_string];
                    }
                }
                else
                {
                    switch (_tmp_value[0])
                    {
                        case '\n':
                            while (char.IsWhiteSpace(_queue.Peek()))
                            {
                                _queue.Dequeue();
                            }
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
                            _tmp_value_string = new string(_tmp_value.ToArray());
                            break;
                        case ')':
                            _value_active.Pop();
                            _tmp_value_string = new string(_tmp_value.ToArray());
                            break;
                        case '"':
                            while (_queue.Count > 0 && _queue.Peek()!='"')
                            {
                                _tmp_value.Add(_queue.Dequeue());
                            }

                            if (_queue.Count>0)
                            {
                                _tmp_value.Add(_queue.Dequeue());
                            }

                            if (_value_active.Peek())
                            {
                                if (_tmp_value.Last()=='"')
                                {
                                    _tmp_value.RemoveAt(_tmp_value.Count-1);
                                }
                                if (_tmp_value.Count > 0 && _tmp_value[0] == '"')
                                {
                                    _tmp_value.RemoveAt(0);
                                }

                                _tmp_value_string = new string(_tmp_value.ToArray());
                                if (!_pairs.ContainsKey(_tmp_value_string))
                                {
                                    _pairs.Add(_tmp_value_string,Next());
                                }

                                _tmp_value_string = "\"" + _pairs[_tmp_value_string] + "\"";
                            }
                            else
                            {
                                _tmp_value_string = new string(_tmp_value.ToArray());
                            }
                            break;
                        case '{':
                            while (_queue.Peek() == ' ')
                            {
                                _queue.Dequeue();
                            }

                            if (_queue.Peek()!='}')
                            {
                                _map_active.Push(true);
                                _value_active.Push(true);
                            }

                            _tmp_value_string = _tmp_value[0].ToString();
                            break;
                        case '}':
                            if (_out_list.Last()!="{")
                            {
                                _map_active.Pop();
                            }
                            _tmp_value_string = _tmp_value[0].ToString();
                            break;
                        case '[':
                            while (_queue.Peek() == ' ')
                            {
                                _queue.Dequeue();
                            }

                            if (_queue.Peek()!=']')
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

                            _tmp_value_string = _tmp_value[0].ToString();
                            break;
                        case ']':
                            if (_out_list.Last()!="[")
                            {
                                _value_active.Pop();
                                _map_active.Pop();
                            }

                            _tmp_value_string = _tmp_value[0].ToString();
                            break;
                        case ',':
                            if (_map_active.Peek())
                            {
                                _value_active.Push(true);
                            }

                            _tmp_value_string = _tmp_value[0].ToString();
                            break;
                        case ':':
                            if (_map_active.Peek())
                            {
                                _value_active.Pop();
                            }

                            _tmp_value_string = _tmp_value[0].ToString();
                            break;
                        default:
                            _tmp_value_string = new string(_tmp_value.ToArray());
                            break;
                    }
                }

                _out_list.Add(_tmp_value_string);
            }

            string tmp = string.Join("", _out_list);
            sw.Stop();
            LastOptimizeTimeTicks = sw.ElapsedTicks;
            AfterLength = tmp.Length;
            return tmp;
            
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
