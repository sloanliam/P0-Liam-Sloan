#! /usr/bin/bash

# Program 1
echo "Running program 1, program 2 will run after."
echo "Program 1: "
read -p "Please enter a number: " number

result=$(( $number % 2 ))

if [ "$result" -eq 0 ]
then
    echo "This is an even number."
elif [ "$result" -eq 1 ]
then
    echo "This number is odd."
fi

# Program 2
read -p "Please input your grade: " grade
if (( "$grade" < 40 ))
then
    echo "Your grade is an F."
elif (( "$grade" > 40 )) && (( "$grade" < 50 ))
then
    echo "Your grade is a D."
elif (( "$grade" > 50 )) && (( "$grade" < 60 ))
then 
    echo "Your grade is a C."
elif (( "$grade" > 60 )) && (( "$grade" < 70 ))
then 
    echo "Your grade is a B."
elif (( "$grade" > 70 ))
then 
    echo "Your grade is an A."
fi