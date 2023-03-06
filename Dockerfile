# test dockerfile

FROM ubuntu:20.04

EXPOSE 5000/tcp
EXPOSE 5000/udp

RUN apt update
RUN apt install -y nano
RUN apt install -y python3
RUN apt install -y python3-pip
RUN pip install Command

RUN mkdir /root/cineca-server
COPY ./cineca-server/ /root/cineca-server/
RUN chmod +x /root/cineca-server/main.py

CMD [ "cd", "/root/cineca-server/", "&&", "python3", "./main.py" ]