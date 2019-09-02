using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EasyBI.DAL.PYTHON
{
	public class Python
	{
		#region JSONtoCSVcodeFommatedCode
		/*
			 * 
			 * 
			 * 
			 * import json
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
					print("-1")*/
		#endregion
		public static string JSONtoCSVcode()
		{
			string dir = @"C:\\Program Files (x86)\\Microsoft Visual Studio\\Shared\\Python36_64\\Lib\\site-packages";

			string python = "import sys\nsys.path.append(r'C:\\Program Files (x86)\\Microsoft Visual Studio\\Shared\\Python36_64\\Lib\\site-packages')\nimport json\n# A partir de un json devuelve un diccionario aplanado de todos los campos de un json yendo de abajo a arriba en la jerarquía,\n# para establecer el nombre de cada campo concatena cada nombre de cada jerarquía  y el nombre mismo del campo.\ndef flatten_json(nested_json):\n    out = {}\n\n    def flatten(x, name=''):\n        if type(x) is dict:\n            for a in x:\n                flatten(x[a], name + a + '_')\n        elif type(x) is list:\n            i = 0\n            for a in x:\n                flatten(a, name + str(i) + '_')\n                i += 1\n        else:\n            out[name[:-1]] = x\n\n    flatten(nested_json)\n    return out\n    \n# Función para leer de forma segura un campo de un diccionario\ndef readField(json,field):\n    try:\n        return json[field]\n    except:\n        return \"\"\n\n# A partir de una lista de jsons devuelve la misma lista de json aplanado en forma de diccionario\ndef createDictionaries(fields,jsonList):\n    dics = []\n    if type(jsonList) == list:\n        for json in jsonList:     \n            dic = getDictionary(fields)\n            flattened_register = flatten_json(json)\n\n            for field in flattened_register:\n                if field in fields:                    \n                    dic[field] = flattened_register[field]\n            dics.append(dic)\n    else:\n        dic = getDictionary(fields)\n        flattened_register = flatten_json(jsonList)\n\n        for field in flattened_register:\n            if field in fields:                    \n                dic[field] = flattened_register[field]\n        dics.append(dic)\n    return dics   \n\n# Pasa de diccionario a una linea de string\ndef dictionaryToString(dictionary):\n    values = []\n    for key in dictionary:\n        values.append('\"' + str(dictionary[key]) + '\"')\n        \n    return ','.join(values)\n\n# Devuelve una lista con los campos de todo el fichero\ndef getFieldsFromJSON(js):\n    fields = []\n    if type(js) == list:\n        for json in js:\n            for field in flatten_json(json):\n                if field not in fields:\n                    fields.append(field)\n    else:\n        for field in flatten_json(js):\n                if field not in fields:\n                    fields.append(field)\n    return fields\n\n# Devuelve un diccionario vacío (solo claves informadas) pasando por parámetros los campos deseados\ndef getDictionary(fields):\n    empty = []\n    for field in fields:\n        empty.append('')\n    dic = dict(zip(fields, empty))\n    return dic\n\n# Devuelve formateado en una fila el campo global de fields\ndef getFieldsLine(fields):\n    f = []\n    \n    for field in fields:\n        f.append('\"' + field + '\"')\n    return ','.join(f)   \n\n#Escribe en un fichero un array con strings\ndef stringToCsv(lines,filename,fields):\n    with open(filename,'w') as file:\n        for line in lines:\n            file.write(line)\n            file.write('\\n')\n            \n\ndef readAndWriteJSON(jsonPath, csvPathOut):\n    js = json.loads(open(jsonPath, \"r\").read())\n    fields = getFieldsFromJSON(js)\n    dictionaries = createDictionaries(fields,js)\n    lines = []\n\n    lines.append(getFieldsLine(fields))\n    for dictionary in dictionaries:\n        lines.append(dictionaryToString(dictionary))\n    \n    stringToCsv(lines, csvPathOut, fields)\n    \n    \n#readAndWriteJSON(\"jsonexample.txt\",\"json1.csv\")\n# Ejecución \nif (len(sys.argv) == 3):\n    try:\n        readAndWriteJSON(str(sys.argv[1]) ,str(sys.argv[2]) )\n        print(\"0\")\n    except:\n        print(\"-1\")\nelse:\n    print(\"-1\")";

			return python;

		}
	}
}
