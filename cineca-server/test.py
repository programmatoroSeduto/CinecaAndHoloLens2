
# from cineca_server_code import storage

# test_struct = {"type":"test","data":[6,4,2,5,3,1]}

# fl = storage.cineca_simple_storage( )
# for i in range(100):
#     fl.store( fl.decode_to_csv( test_struct ) )

# fl.close()

import command 

def debugger( txt ):
    print( txt )

# per eseguire il comando
# res = command.run('ls -la') # non funziona
# res = command.run(['ls', '-la'])
res = command.run(['ls'], debug=debugger) 

# output a schermo
# print(res.output.decode('ascii'))
# err code
print(res.exit) 