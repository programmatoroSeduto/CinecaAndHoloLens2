#!/bin/bash


# === try finding the repo inside the machine
base=$(pwd)
if [ ! -d "/root/cineca-hl2-repo/cineca-server" ]; then
echo "ERROR: repository not found!"
echo -e "\tPlease download the repo first."
exit 1
fi

cd /root/cineca-hl2-repo/cineca-server


# === set the token from command line
token=$1
if [ -z "${token}" ]; then
echo -e "USAGE:\n\tbuild.sh <your GIT token>"
exit 1
fi
echo -n "${token}" > ./credentials/token


# === delete previous versions of the image
docker_img_tag="cineca-docker-image"
img_search=$(docker images | grep "${docker_img_tag}" | awk '{print $1}')
if [ "${img_serch}" == "${docker_img_tag}" ]; then
echo "found imange '$img_search' -- removing ..."
docker image rm $img_search
echo "OK"
fi


# === build the image
echo "building Docker image..."
docker build -t "$docker_img_tag:latest" -f ./Dockerfile .
echo "OK"
echo -e "\ndocker images right now"
docker images


cd $base

# === end
echo "Done!"
rm -f ./credentials/token