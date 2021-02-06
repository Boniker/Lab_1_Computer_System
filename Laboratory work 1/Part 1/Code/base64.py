import os, re, math, base64, zipfile

# Paths to files
pathFile_1 = "D:\Study University Borys Bilkevych 3 course 2 semestr\Computer System\Laboratory works\Laboratory work 1\Part 1\First\Acid.txt"
pathFile_2 = "D:\Study University Borys Bilkevych 3 course 2 semestr\Computer System\Laboratory works\Laboratory work 1\Part 1\Second\Lina Kostenko.txt"
pathFile_3 = "D:\Study University Borys Bilkevych 3 course 2 semestr\Computer System\Laboratory works\Laboratory work 1\Part 1\Third\Phishing.txt"

# Paths to archives
pathArchive_1 = "D:\Study University Borys Bilkevych 3 course 2 semestr\Computer System\Laboratory works\Laboratory work 1\Part 1\First\Acid.txt.bz2"
pathArchive_2 = "D:\Study University Borys Bilkevych 3 course 2 semestr\Computer System\Laboratory works\Laboratory work 1\Part 1\Second\Lina Kostenko.txt.bz2"
pathArchive_3 = "D:\Study University Borys Bilkevych 3 course 2 semestr\Computer System\Laboratory works\Laboratory work 1\Part 1\Third\Phishing.txt.bz2"

def OutputTheResultsForFile(path):
    text = ReadFile(path)
    print(text)
    base64encode(text)

def OutputTheResultsForArcives(pathfile, patharch, str):
    text = ReadFile(pathfile)

def base64encode(s):
    i = 0
    base64 = ending = ''
    base64chars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/'
  
    # Add padding if string is not dividable by 3
    pad = len(s) % 3
    if pad != 0:
        while pad < 3:
            s += "A"
            ending += '='
            pad += 1
  
    # Iterate though the whole input string
    while i < len(s):
        b = 0
 
        # Take 3 characters at a time, convert them to 4 base64 chars
        for j in range(0,3,1):
      
            # get ASCII code of the next character in line
            n = ord(s[i])
            i += 1
  
            # Concatenate the three characters together 
            b += n << 8 * (2-j)
    
        # Convert the 3 chars to four Base64 chars
        base64 += base64chars[ (b >> 18) & 63 ]
        base64 += base64chars[ (b >> 12) & 63 ]
        base64 += base64chars[ (b >> 6) & 63 ]
        base64 += base64chars[ b & 63 ]
 
    # Add the actual padding to the end
    base64 += ending
  
    # Print the Base64 encoded result
    print (base64)

    return base64

#Read from file
def ReadFile(path):
    if not os.path.exists(path):
        print("File not exist")
        return

    with open(path, "r", encoding='utf-8') as f:
        text = f.read()
    return text

#Read from archive
def ReadArchive(path):
    listOfByte=[]
    with open(path, "rb") as f:
        array = f.read()
    for byte in array:
        listOfByte.append(ConvertToBit(byte))
    return listOfByte

#Outputs for file
OutputTheResultsForFile(pathFile_1)
#OutputTheResultsForFile(pathFile_2)
#OutputTheResultsForFile(pathFile_3)

#Outputs for archives
#OutputTheResultsForArcives(pathFile_1, pathArchive_1, "Acid.txt.bz2")
#OutputTheResultsForArcives(pathFile_2, pathArchive_2, "Lina Kostenko.txt.bz2")
#OutputTheResultsForArcives(pathFile_2, pathArchive_3, "Phishing.txt.bz2")