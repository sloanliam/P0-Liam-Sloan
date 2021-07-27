## Basic Git and commands
- `cd 'path'` -> change directory
- `cd ..` -> moves a level up
	- use arrow keys to see history of previously use commands
	- history -> gives a track of all commands you have used in the current session
- `cd ./ tab key` -> gives the auto-completion of the file/directory
- `ls` -> list all the files and directories
- `mkdir 'directory name'` -> make directory
- `touch 'file name.extension'` -> creates a file
- `git clone 'url'` -> adds the local workspace in your machine
- `git add "filename"` -> adds the file you target to add to git
- `git add .` -> adds all the file
- `git commit -m 'message'` -> Stage changes and commit to git as a new node
- `git push` -> push changes to git server
- `git status` -> see the new tracks/ changes made in local workspace
- `git pull` -> retrieve changes from the git server (updates your workspace with latest code)
- `pwd` -> print where directory

### Demo
- `cd C:/Revature`   - to navigate to a directory
- `cd 210726-wvu-net-ext/`
- `git clone https://github.com/210726-wvu-net-ext/training-code.git` - to make a local copy of the repo
- `cd training-code/`
- `mkdir 01Scripting` - create a folder/directory
- `cd 01Scripting/`
- `touch Notes.md` - create a file
- `notepad Notes.md` - open the file with notepad
	- add some content
- `git statu`s - to check changes if any
- `git add` . - stage the changes in all the file(s)
- `git commit -m 'add notes for git basics'` - create a tracking node
- `git push` - add changes to git origin/server
- `git pull` - to get changes from the server

## Shell File Management
- `ls`  - listing files
- `ls -l` - get more information about the listed files