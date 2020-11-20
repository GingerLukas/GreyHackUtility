# GreyHackUtility
## Compiler
### Usage
* The compiler is able to include local classes and include classes from github to some extend with syntax #!git/__author/path/branch/file__!
(you can modify this behvior in Include function of the compiler, I am looking forward to any improvements)
* The include button will include any classes specified with syntax __#!Classname!__ or #!git/__author/path/branch/file__!
* The optimize button will shorten your code, so it can fit into 80k char limit of GreyHack, the functionality of the script stays the same, just note that all the variables and methods will be renamed. 99% of playerbase code should work just fine but some methods of "code reflection" may need some workarounds (compile time code for exluding specific method/variable names is planned)
