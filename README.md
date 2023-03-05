# CinecaAndHoloLens2

## Aim of the project

Main aim of the project is to *test (a bit) the combination between a server and HoloLens2* used in a very basic way. 

## Repo Structure

Four branches:

1. *main* -- readme and installation utilities
2. *server* -- the Docker image and the server application
3. *client* -- some Cs code to run on HoloLens2
4. *out* -- the server will write here the file generated by the communication with HoloLens2
5. *test_client* -- in order to test the server side before starting the development for HoloLens2, I preferred write some simple code

## Project Setup

### Server Side

Here are the steps to install the Docker container inside ADA Cloud:

1. access ADA Cloud through Secure SHell
2. download this repository (*branch main*)
3. run the bash script *./build.sh* (the script needs the token)
   - it downloads the repository inside the folder */root/cineca-hl2-repo*
4. create the Docker container using *download.sh* (again, the token is required)
   - the name of the container will be *cineca-docker-image*
5. the last step is to run the Docker container, using the bash script *run.sh*
   - the container will be named *cineca-server*
   - *ATTENTION*: the script could not to work properly

### Client Side

*tbd.*