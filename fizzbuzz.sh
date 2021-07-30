#! /usr/bin/bash

for n in {1..20}
do
    fizznumber=$(( n % 3 ))
    fizzbuzznumber=$(( n % 5 ))
    if [ "$fizznumber" -eq 0 ]
    then 
        echo "Fizz"
    fi
    if [ "$fizzbuzznumber" -eq 0 ]
    then
        echo "fizzbuzz"
    fi
done