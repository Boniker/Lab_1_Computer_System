import os, re, math

# Paths
pathFile_1 = "D:\Study University Borys Bilkevych 3 course 2 semestr\Computer System\Laboratory works\Laboratory work 1\Part 1\First\Acid.txt"
pathFile_2 = "D:\Study University Borys Bilkevych 3 course 2 semestr\Computer System\Laboratory works\Laboratory work 1\Part 1\Second\Lina Kostenko.txt"
pathFile_3 = "D:\Study University Borys Bilkevych 3 course 2 semestr\Computer System\Laboratory works\Laboratory work 1\Part 1\Third\Phishing.txt"

dictionary = "АБВГҐДЕЄЖЗИІЇЙКЛМНОПРСТУФХЦЧШЩЬЮЯ"
countOfAllLetters = 0  # Count of all letters
array = []

#Output the result
def OutputTheResults(path, arr, vocabulary):
    global countOfAllLetters
    global array

    text = ReadFile(path)
    print(text) 
    count = LettersInText(arr, text, vocabulary)  # Counts
    LettersFrequency(arr, count)  # Letters Frequency
    amountOfInformation = CountEntropy(arr, path, count)  # Entropy

    # Compare sizes of different types of archive
    CompareWithSizeOfArchive(amountOfInformation, path)
    ShowArray(arr, vocabulary)
    array = []

#Read file
def ReadFile(path):
    if not os.path.exists(path):
        print("File not exist")
        return
    with open(path, "r", encoding='utf-8') as f:
        text = f.read()
    return text

# Count Letters in Text
def LettersInText(array, text, dictionary):
    global countOfAllLetters
    
    text = re.sub(r'\W', "", text)
    text = re.sub(r'\d', "", text)
    text = re.sub(r'[a-zA-Z]', "", text).upper()
    lettercount = len(text)
    countOfAllLetters += lettercount

    for i in range(len(dictionary)):
        letters = text.count(dictionary[i])
        if len(array) > len(dictionary):
            array[i][0]=+letters
        else:
            array.append([letters])
    print("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++")
    print(f"\nAll characters in text = {lettercount}")
    return lettercount

#Frequecy of letters
def LettersFrequency(array, count):
    global countOfAllLetters

    for member in array:
        member.append((member[0]/count))

#CountEntropy
def CountEntropy(array, path, count):
    entropy = 0

    for i in range(33):
        if array[i][0]:
            entropy += array[i][1] * math.log(1 / array[i][1], 2)

    file_size = os.path.getsize(path)
    amountOfInformation = entropy * count / 8

    print(f"Averange entropy: {round(entropy, 4)} біт", entropy)
    print(f"Count of information: {round(entropy*count, 4)} bit")
    print(f"Count of information: {round(amountOfInformation, 4)} byte\n")
    print(f"File size: {file_size} byte")

    if file_size > amountOfInformation:
        print("File size > Count of information\n")
    elif file_size == amountOfInformation:
        print("File size = Count of information\n")
    else:
        print("File size < Count of information\n")
    return amountOfInformation

# Output the array with letters count
def ShowArray(array, dictionary):
    print("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n| Letter|\t|Counts|\t|Frequency|")
    for i in range(len(dictionary)):
        print(f"| {dictionary[i]} \t\t| {array[i][0]}\t| \t| {round(array[i][1]*100, 4)}")
    print("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++")

#Compare diffrent archive sizes and count of info
def CompareWithSizeOfArchive(amountOfInformation, path):
    archive = [ ".rar", ".zip", ".gz", ".bz2", ".7z" ]
    for extention in archive:
        file_size = os.path.getsize(path+extention)

        print(f"Archive size {extention}: {file_size}")
        if file_size > amountOfInformation:
            print(f"Archive size {extention} > Count of information")
        elif file_size == amountOfInformation:
            print(f"Archive size {extention} == Count of information")
        else:
            print(f"Archive size {extention} < Count of information")

# Outputs
OutputTheResults(pathFile_1, array, dictionary)
OutputTheResults(pathFile_2, array, dictionary)
OutputTheResults(pathFile_3, array, dictionary)