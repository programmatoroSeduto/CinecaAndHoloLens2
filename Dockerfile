# test dockerfile

FROM ubuntu:18.04

EXPOSE 5000/tcp
EXPOSE 5000/udp

RUN apt update
RUN apt install -y python3
RUN apt install -y nano

COPY ./server.py /root/server.py
RUN chmod -x /root/server.py

CMD [ "python3", "/root/server.py", "5000" ]