import sys, math 

# Paths to files
pathFile_1 = "D:\Study University Borys Bilkevych 3 course 2 semestr\Computer System\Laboratory works\Laboratory work 1\Part 1\First\Acid.txt"
pathFile_2 = "D:\Study University Borys Bilkevych 3 course 2 semestr\Computer System\Laboratory works\Laboratory work 1\Part 1\Second\Lina Kostenko.txt"
pathFile_3 = "D:\Study University Borys Bilkevych 3 course 2 semestr\Computer System\Laboratory works\Laboratory work 1\Part 1\Third\Phishing.txt"

# Paths to archives
pathArchive_1 = "D:\Study University Borys Bilkevych 3 course 2 semestr\Computer System\Laboratory works\Laboratory work 1\Part 1\First\Acid.txt.bz2"
pathArchive_2 = "D:\Study University Borys Bilkevych 3 course 2 semestr\Computer System\Laboratory works\Laboratory work 1\Part 1\Second\Lina Kostenko.txt.bz2"
pathArchive_3 = "D:\Study University Borys Bilkevych 3 course 2 semestr\Computer System\Laboratory works\Laboratory work 1\Part 1\Third\Phishing.txt.bz2"

def main():
    print('\nEntropy first archive\n')
    entropy(ReadBinary(pathArchive_1))
    print('\nEntropy first archive base 64\n')
    entropy(base64encode(ReadBinary(pathArchive_1)))

    print('\nEntropy second archive\n')
    entropy(ReadBinary(pathArchive_2))
    print('\nEntropy second archive base64\n')
    entropy(base64encode(ReadBinary(pathArchive_2)))

    print('\nEntropy third archive\n')
    entropy(ReadBinary(pathArchive_3))
    print('\nEntropy third archive base64\n')
    entropy(base64encode(ReadBinary(pathArchive_3)))

    print('\nEntropy first file base64\n')
    entropy(base64encode(ReadBinary(pathFile_1)))
    print('\nEntropy second file base64\n')
    entropy(base64encode(ReadBinary(pathFile_2)))
    print('\nEntropy third file base64\n')
    entropy(base64encode(ReadBinary(pathFile_3)))


def base64encode(s):
    i = 0
    base64 = ending = ''
    base64chars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/'
  
    # Add padding if string is not dividable by 3
    pad = len(s) % 3
    if pad != 0:
        while pad < 3:
            s.append(192)
            ending += '='
            pad += 1

    # Iterate though the whole input string
    while i < len(s):
        b = 0

        # Take 3 characters at a time, convert them to 4 base64 chars
        for j in range(0,3,1):

            # get ASCII code of the next character in line
            n = s[i]
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
    print (f'{base64}\n')
    
    #print(f'Entropy of base64 === {shannon(base64)}\n Count of Information === {shannon(base64)*len(base64)/8}')
    b = bytearray()
    b.extend(map(ord, base64))
    return b

def ReadBinary(path):
    print('Opening file...')
    with open(path, 'rb') as f:
        byteArr = list(f.read())
    return byteArr

def entropy(byteArr):
    
    fileSize = len(byteArr)

    print('File size in bytes: {:,d}'.format(fileSize))
    # calculate the frequency of each byte value in the file
    print('Calculating Entropy of file. Please wait...')
   
    freqList = []
    for b in range(256):
        ctr = 0
        for byte in byteArr:
            if byte == b:
                ctr += 1
        freqList.append(float(ctr) / fileSize)
    
    # Entropy
    ent = 0.0
    for freq in freqList:
        if freq > 0:
            ent = ent + freq * math.log(freq, 2)
    ent = -ent

    print(f'Shannon entropy: {ent}')
    print(f'Amount of Information: {ent*len(byteArr)/8}')

if __name__== "__main__":
    main()