#!/bin/bash

base=$(pwd)

token=$1
if [ -z $token ]; then
echo -e "USAGE:\n\tdownload.sh <your GIT token>"
exit 1
fi

git_user="programmatoroSeduto"
git_repo="CinecaAndHoloLens2"
git_branch="server"
git_url="https://${token}@github.com/${git_user}/${git_repo}.git"

if [ -d "/home/cineca-hl2-repo" ]; then
rm -rf /home/cineca-hl2-repo
fi

echo "cloning repo..."
mkdir /home/cineca-hl2-repo
git clone ${git_url} -b ${git_branch} /home/cineca-hl2-repo
cd /home/cineca-hl2-repo
echo "OK"

git remote set-url origin ${git_url}
git pull origin server

echo "done"
cd $base