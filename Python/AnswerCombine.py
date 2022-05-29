from fileinput import filename
import pandas as pd
import csv
import glob

def Combine(filepath):
    df = pd.DataFrame(columns = [])

    for i in glob.glob("*/Answer/"+filepath):
        tmp = pd.read_csv(i)
        df = pd.concat([df, tmp])

    df.to_csv(filepath, index = False)

def Proportion(filename,lr):
    answerData = open("Answer_"+filename, "r", encoding="ms932", errors="", newline="" )
    f_answer = csv.reader(answerData, delimiter=",", doublequote=True, lineterminator="\r\n", quotechar='"', skipinitialspace=True)

    if lr == "l":
        Proportion = [[x / 100 , 0.0] for x in range(-60, -5, 5)]
        count = [[x / 100, 0.0] for x in range(-60, -5, 5)]
    elif lr == "r":
        Proportion = [[x / 100 , 0.0] for x in range(10, 65, 5)]
        count = [[x / 100 , 0.0] for x in range(10, 65, 5)]

    i = 0
    for row in f_answer:
        if row != []:
            if row[1] != "answer":
                row[0] = float(row[0])
                if row[0] < 0:
                    j = int(row[0]/0.05 + 12)
                    if row[1] == "1":
                        Proportion[j][1] += 1.0
                    count[j][1] += 1.0
                else:
                    j = int(round(row[0]/0.05-2))
                    if row[1] == "1":
                        Proportion[j][1] += 1.0
                    count[j][1] += 1.0

    for i in range(11):
        try:
            Proportion[i][1] /= count[i][1]
        except ZeroDivisionError:
            print("ZeroDivisionError")
            Proportion[i][1] = -1

    with open("Proportion_"+filename, 'w', newline = '') as file:
        writer = csv.writer(file)
        writer.writerows(Proportion)


Combine("Answer_left_1.csv")
Combine("Answer_left_5.csv")
Combine("Answer_left_125.csv")
Combine("Answer_right_1.csv")
Combine("Answer_right_5.csv")
Combine("Answer_right_125.csv")
Proportion("left_1.csv","l")
Proportion("left_5.csv","l")
Proportion("left_125.csv","l")
Proportion("right_1.csv","r")
Proportion("right_5.csv","r")
Proportion("right_125.csv","r")
