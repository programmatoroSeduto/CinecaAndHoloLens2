#!/bin/bash

# set the token from command line
token=$1
if [ -z "${token}" ]; then
echo -e "USAGE:\n\tbuild.sh <GIT TOKEN>"
exit 1
fi

echo -n "creating token file..."
echo -n "${token}" > ./credentials/token
echo " OK"


# build docker image