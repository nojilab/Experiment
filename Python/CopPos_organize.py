from datetime import datetime as dt
from datetime import timedelta
import csv

def Str_to_Time_experiment(f,i):
    l = []
    for row in f:
        if row != []:
            if row[i] != "worldtime" and row[i] != "approachingtime":
                print(row[i])
                row[i] = dt.strptime(row[i], '%Y-%m-%d %H:%M:%S.%f')
                l.append(row)
    return l

name = input("name:")
CopPosData = open("./"+name+"/CopPos/CopPosSaveData.csv", "r", encoding="ms932", errors="", newline="" )
bulletTime = open("./"+name+"/Bullet/organizedBullet.csv", "r", encoding="ms932", errors="", newline="" )
f_cp = csv.reader(CopPosData, delimiter=",", doublequote=True, lineterminator="\r\n", quotechar='"', skipinitialspace=True)
f_bullet = csv.reader(bulletTime, delimiter=",", doublequote=True, lineterminator="\r\n", quotechar='"', skipinitialspace=True)

# tdatetime = dt.strptime(f, '%Y/%m/%d %H:%M:%S.%f')

header = next(f_cp) #CopPos 0:x 1:y 2:time
header = next(f_bullet) #bullet 8:approachingtime 10:close
f_cp = Str_to_Time_experiment(f_cp,2)
f_bullet = Str_to_Time_experiment(f_bullet,8)
CopPos_close = []
standard = []
CopPos_not_close = []
CopPos_standard = []
for row1 in f_bullet:
    if row1[10] == "1":
        print(row1[8])
        for row2 in f_cp:
            dt0 = row1[8] - timedelta(milliseconds=6000)
            dt1 = row1[8] - timedelta(milliseconds=2000)
            dt2 = row1[8] + timedelta(milliseconds=2000)
            if dt0 <= row2[2] <= dt1:
                CopPos_close.append(row2)
            elif dt1 <= row2[2] <= dt2:
                standard.append(row2)
    elif row1[10] == "0":
        print(row1[8])
        for row3 in f_cp:
            dt0 = row1[8] - timedelta(milliseconds=6000)
            dt1 = row1[8] - timedelta(milliseconds=2000)
            dt2 = row1[8] + timedelta(milliseconds=2000)
            if dt0 <= row3[2] <= dt1:
                CopPos_not_close.append(row3)
            elif dt1 <= row3[2] <= dt2:
                CopPos_standard.append(row3)


with open("./"+name+"/organizedCopPosData.csv", 'w', newline = '') as file1:
    writer = csv.writer(file1)
    writer.writerows(CopPos_close)

with open("./"+name+"/standardCopPosData.csv", 'w', newline = '') as file2:
    writer = csv.writer(file2)
    writer.writerows(standard)

with open("./"+name+"/notcloseCopPos.csv", 'w', newline = '') as file3:
    writer = csv.writer(file3)
    writer.writerows(CopPos_not_close)

with open("./"+name+"/notclosestandardCopPos.csv", 'w', newline = '') as file4:
    writer = csv.writer(file4)
    writer.writerows(CopPos_standard)
