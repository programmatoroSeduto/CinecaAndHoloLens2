#!/bin/bash

git_token=$(cat ./credentials/token)
if [ -z $git_token ]; then
echo "ERROR: token not found"
exit 1
fi

if [ -d "repo" ]; then
rm -rf ./repo
fi
mkdir repo

git_user="programmatoroSeduto"
git_repo="CinecaAndHoloLens2"
git_branch="server"
git_url="https://${git_token}@github.com/${git_user}/${git_repo}.git"

git clone ${git_url} -b out ./repo
cd repo
git remote add origin ${git_url}

rm -f out.csv
cp ../cineca_storage.csv out.csv

git add .
git commit -m "output from server"
git push origin out

cd ..
rm -rf repo