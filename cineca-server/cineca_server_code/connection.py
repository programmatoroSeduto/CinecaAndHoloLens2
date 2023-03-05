
import socket

## connection.py
#    utilities to manage connection between the device and the server

def init_connection( portno=5000 ):
    ''' create and bind the socket
    '''

    sock = socket.socket( socket.AF_INET, socket.SOCK_STREAM )
    sock.setsockopt( socket.SOL_SOCKET, socket.SO_REUSEADDR, 1 )
    sock.bind( ( socket.gethostname(), portno ) )
    sock.listen(5)

    return sock


def wait_connection( sock : socket.socket ):
    ''' wait a connection from the client
    '''

    ( cl, _ ) = sock.accept(  )
    return cl