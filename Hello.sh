# /usr/bin/bash

echo "What are your top 3 favorite movies?"
read -a movies
echo "You chose:"
echo "${movies[0]}"
echo "${movies[1]}"
echo "${movies[2]}"
echo "These are all great movies."