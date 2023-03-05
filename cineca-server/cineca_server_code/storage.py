
import json
import os

class cineca_simple_storage:
    ''' the class creates a CSV file and populates it
    in append. 
    '''

    def __init__( self ):
        self.__filename = "cineca_storage.csv"
        self.__id = 0
        with open( self.__filename, "w" ):
            pass
        self.__fil = open( self.__filename, "a" )

    def decode_to_csv( self, j : dict ):
        # row ID
        id = self.__id
        self.__id = self.__id + 1

        # row data
        row = f"{id},"
        for i in range(len(j["data"])):
            row += str(j["data"][i]) + ","
        row = row[:len(row)-1] + "\n"

        # return the line
        return row
    
    def store( self, line : str ):
        self.__fil.write( line )
        self.__fil.flush( )

    def close( self ):
        self.__fil.close()

