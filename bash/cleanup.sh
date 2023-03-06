#!/bin/bash

cd
rm -rf cineca
sudo rm -rf /home/cineca-hl2-repo
docker container rm cineca-server
docker image rm cineca-docker-image