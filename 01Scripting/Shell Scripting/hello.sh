#! /usr/bin/bash

#this how we write comments
: '
this is
multiline
comment 
'

echo "Hello World"  #this ia also a comment 

# System defined Variables
echo Our Shell name is $BASH
echo Our Shell version is $BASH_VERSION
echo Our Home directory is $HOME
echo Our current working directory is $PWD

# User Defined Variables
name=Pushpinder
echo The name is $name

#_10num=10
#echo $_10num

name=Fred
echo The changed name - $name

pi=3.14
readonly pi #this locks the value and cannot be unset later
unset pi
echo the value of pi is $pi
#pi=22.7