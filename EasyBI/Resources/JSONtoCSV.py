import json
import sys
# A partir de un json devuelve un diccionario aplanado de todos los campos de un json yendo de abajo a arriba en la jerarquía,
# para establecer el nombre de cada campo concatena cada nombre de cada jerarquía  y el nombre mismo del campo.
def flatten_json(nested_json):
    out = {}

    def flatten(x, name=''):
        if type(x) is dict:
            for a in x:
                flatten(x[a], name + a + '_')
        elif type(x) is list:
            i = 0
            for a in x:
                flatten(a, name + str(i) + '_')
                i += 1
        else:
            out[name[:-1]] = x

    flatten(nested_json)
    return out
    
# Función para leer de forma segura un campo de un diccionario
def readField(json,field):
    try:
        return json[field]
    except:
        return ""

# A partir de una lista de jsons devuelve la misma lista de json aplanado en forma de diccionario
def createDictionaries(fields,jsonList):
    dics = []
    if type(jsonList) == list:
        for json in jsonList:     
            dic = getDictionary(fields)
            flattened_register = flatten_json(json)

            for field in flattened_register:
                if field in fields:                    
                    dic[field] = flattened_register[field]
            dics.append(dic)
    else:
        dic = getDictionary(fields)
        flattened_register = flatten_json(jsonList)

        for field in flattened_register:
            if field in fields:                    
                dic[field] = flattened_register[field]
        dics.append(dic)
    return dics   

# Pasa de diccionario a una linea de string
def dictionaryToString(dictionary):
    values = []
    for key in dictionary:
        values.append('"' + str(dictionary[key]) + '"')
        
    return ','.join(values)

# Devuelve una lista con los campos de todo el fichero
def getFieldsFromJSON(js):
    fields = []
    if type(js) == list:
        for json in js:
            for field in flatten_json(json):
                if field not in fields:
                    fields.append(field)
    else:
        for field in flatten_json(js):
                if field not in fields:
                    fields.append(field)
    return fields

# Devuelve un diccionario vacío (solo claves informadas) pasando por parámetros los campos deseados
def getDictionary(fields):
    empty = []
    for field in fields:
        empty.append('')
    dic = dict(zip(fields, empty))
    return dic

# Devuelve formateado en una fila el campo global de fields
def getFieldsLine(fields):
    f = []
    
    for field in fields:
        f.append('"' + field + '"')
    return ','.join(f)   

#Escribe en un fichero un array con strings
def stringToCsv(lines,filename,fields):
    with open(filename,'w') as file:
        for line in lines:
            file.write(line)
            file.write('\n')
            

def readAndWriteJSON(jsonPath, csvPathOut):
    js = json.loads(open(jsonPath, "r").read())
    fields = getFieldsFromJSON(js)
    dictionaries = createDictionaries(fields,js)
    lines = []

    lines.append(getFieldsLine(fields))
    for dictionary in dictionaries:
        lines.append(dictionaryToString(dictionary))
    
    stringToCsv(lines, csvPathOut, fields)
    
    
#readAndWriteJSON("jsonexample.txt","json1.csv")
# Ejecución 
if (len(sys.argv) == 3):
    try:
        readAndWriteJSON(str(sys.argv[1]) ,str(sys.argv[2]) )
        print("0")
    except:
        print("-1")
else:
    print("-1")