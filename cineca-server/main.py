
from cineca_server_code import connection
from cineca_server_code import storage
import json
# from command import Command
import os
import subprocess

def main():
    ''' the main execution code of the server
    
    '''
    print( "current path is:", os.getcwd() )
    os.chdir( "/root/cineca-server/" )
    print( "(after chdir()) current path is:", os.getcwd() )
    
    print("init storage...")
    fl = storage.cineca_simple_storage( )

    print( "binding to port 5000 ..." )
    sk = connection.init_connection( portno=5000 )

    print( "BEGIN CYCLE" )
    while True:
        print( "waiting connection ... " )
        cl = connection.wait_connection( sk )

        print( "waiting package ..." )
        pack = cl.recv( 1024 ).decode('ascii')

        print( "decoding..." )
        j = json.loads( pack )
        if j["type"].lower() == "dump":
            break
        
        print( "storing record..." )
        fl.store( fl.decode_to_csv( j ) )

        print( "closing connection..." )
        cl.close()
    
    print( "sending results to GIT repo..." )
    subprocess.call( '/root/cineca-server/bash/send_to_out.sh' )

    print( "closing (end of the job) ..." )
    fl.close()
    sk.close()




        


if __name__ == "__main__":
    try:
        main()
    except KeyboardInterrupt:
        print( "Closing (requeste by user) ..." )