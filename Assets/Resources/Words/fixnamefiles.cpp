#include <iostream>
#include <fstream>
#include <queue>
#include <stdio.h>
#include <ctype.h>
#include <algorithm>
#include <string>
using std::queue;
using std::string;
using std::ifstream;
std::ofstream out;
using std::endl;
using std::cout;
using std::cin;
using std::find;

int main(){
    string take_from;
    string put_in;
    
    cout << "Entire name_file that needs to be fixed: ";
    getline(cin, take_from);
    cout << "\nEnter a name for your new name_file: ";
    getline(cin, put_in);
    
    ifstream myfile;
    myfile.open(take_from);
    queue<string> female_names;

    string line;
    out.open(put_in);
    while(getline(myfile, line)){
        string temp = line.substr(0,line.find(" ", 0));
        std::transform(temp.begin(), temp.end(), temp.begin(), ::tolower);
        std::transform(temp.begin(), temp.begin()+1, temp.begin(), ::toupper);
        
        int h = std::count(temp.begin(), temp.end(), '-');
        
        std::size_t found = temp.find("-");
        int m = 0;
        for(int m = 0; m<temp.length(); ++m){
            if(temp.at(m)=='-'){
                std::transform(temp.begin()+m+1, temp.begin()+m+2, temp.begin()+m+1, ::toupper);
            }
        }
        if(temp.substr(0, 2)=="Mc"){
            cout << "ayyyy!\n";
            std::transform(temp.begin()+2, temp.begin()+3, temp.begin()+2, ::toupper);
        }
        
        out<<temp<<endl;
    }
    myfile.close();
    
    
    return 0;
}