#!/usr/bin/python3 

from urllib.request import urlopen
import datetime
import sys

#Do a pass arguments thing
	#URL
	#Date/File format
	#Path

#Default URL (Make this an Argument)
url = 'https://www.navcen.uscg.gov/?pageName=iipB12Out'

#Don't need (Is for testing)
file = 'garbagehtml.txt'

#Connection and download, string conversion
data = urlopen(url).read()
data = str(data)

#Writes to the file (For testing)
f = open(file, 'w')
f.write(data)

#Actual Parser itself (Could clean up, just tested each step one at a time)
#Need to add back begginig and end or find different split points
parse = open(file, 'rt').read()
first, parse = parse.split('1.')
parse, second = parse.split('CANCEL THIS')
parse = parse.replace('<p>', '')
parse = parse.replace('</p>', '')
parse = parse.replace('\\n' , '')
parse = parse.replace('\\t' , '')

#For testing
print(parse)

# Date time to name the output file. 
#Pass arguments would be prefered but could have this as backup or defualt
date = datetime.datetime.now()
date = str(date.year) + '-' + str(date.month) + '-' + str(date.day) + '_'
fileout = date + 'bulletin_pull.txt'

#For testing
print(fileout)

#Writes to the Bulletin file
f = open(fileout, 'w')
f.write(parse)

