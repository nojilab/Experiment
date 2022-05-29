from datetime import datetime as dt
from datetime import timedelta
import csv

name = input("name:")
answerData = open("./"+name+"/Answer/AnswerData.csv", "r", encoding="ms932", errors="", newline="" )
bulletTime = open("./"+name+"/Bullet/BulletData.csv", "r", encoding="ms932", errors="", newline="" )
f_answer = csv.reader(answerData, delimiter=",", doublequote=True, lineterminator="\r\n", quotechar='"', skipinitialspace=True)
f_bullet = csv.reader(bulletTime, delimiter=",", doublequote=True, lineterminator="\r\n", quotechar='"', skipinitialspace=True)

# tdatetime = dt.strptime(f, '%Y/%m/%d %H:%M:%S.%f')

header = next(f_answer) #answer 4:answer
header = next(f_bullet) #bullet 8:approachingtime 10:close
l_answer = list(f_answer)

bulletAnswerCombine=[]
i = 0
for row1 in f_bullet:
    print(row1)
    if row1 != []:
        if row1[8] != "approachingtime":
            print(l_answer[i])
            print(i)
            row1.append(l_answer[i][4])
            bulletAnswerCombine.append(row1)
    i = i+1


with open("./"+name+"/Bullet/organizedBullet.csv", 'w', newline = '') as file1:
    writer = csv.writer(file1)
    writer.writerows(bulletAnswerCombine)