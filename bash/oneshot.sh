#!/bin/bash

if [ -z $token ]; then
	echo "ERROR: token variable undefined!"
	exit 1
fi

cd

git clone https://${token}@github.com/programmatoroSeduto/CinecaAndHoloLens2.git \
	-b main ./cineca && \
cd cineca/bash && \
chmod +x * && \
sudo ./download.sh ${token} && \
sudo ./build.sh ${token} && \
sudo ./run.sh && \
cp && \
./cleaup.sh