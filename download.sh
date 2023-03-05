#!/bin/bash

token=$1
if [ -z $token ]; then
echo -e "USAGE:\n\t <script name>.sh <token>"
exit 1
fi

git_user="programmatoroSeduto"
git_repo="CinecaAndHoloLens2"
git_branch="server"
git_url="https://${token}@github.com/${git_user}/${git_repo}.git"

mkdir repo
git clone ${git_url} -b ${git_branch} ./repo
cd repo

git remote add origin ${git_url}
git pull
git pull
git pull