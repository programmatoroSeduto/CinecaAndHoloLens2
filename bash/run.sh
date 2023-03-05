#!/bin/bash

if [ ! -d "/home/cineca-hl2-repo" ]; then
echo "ERROR: repository not found!"
echo -e "\tPlease install the server code first."
exit 1
fi

# === remove previously registered versions of the container
docker_container_tag="cineca-server"
docker_container_cmd=$(docker container list -a | grep "${docker_container_tag}" | awk '{print $1}')
if [ -d "${docker_container_cmd}" == "${docker_container_tag}" ]; then
docker container rm "${docker_container_cmd}"
fi


# === run the new container in interacive mode
docker_img_tag="cineca-docker-image"
docker run -it --name="${docker_container_tag}" -p 5000:5000 ${docker_img_tag}