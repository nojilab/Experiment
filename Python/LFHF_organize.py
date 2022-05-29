from datetime import datetime as dt
from datetime import timedelta
import csv

def Str_to_Time_experiment(f,i):
    l = []
    for row in f:
        row[i] = dt.strptime(row[i], '%Y-%m-%d %H:%M:%S.%f')
        l.append(row)
    return l

def Str_to_Time_mybeat(f,i):
    l = []
    for row in f:
        row[i] = dt.strptime(row[i], '%Y/%m/%d %H:%M:%S.%f')
        l.append(row)
    return l

name = input("name:")
lfhfData = open("./"+name+"/RRI/LFHF.csv", "r", encoding="ms932", errors="", newline="" )
bulletTime = open("./"+name+"/Bullet/organizedBullet.csv", "r", encoding="ms932", errors="", newline="" )
f_lfhf = csv.reader(lfhfData, delimiter=",", doublequote=True, lineterminator="\r\n", quotechar='"', skipinitialspace=True)
f_bullet = csv.reader(bulletTime, delimiter=",", doublequote=True, lineterminator="\r\n", quotechar='"', skipinitialspace=True)

# tdatetime = dt.strptime(f, '%Y/%m/%d %H:%M:%S.%f')

header = next(f_lfhf) #lfhf 0:time 5:lf/hf
#header = next(f_bullet) #bullet 8:approachingtime 10:close
f_lfhf = Str_to_Time_mybeat(f_lfhf,0)
f_bullet = Str_to_Time_experiment(f_bullet,8)
lfhf_close = []
standard = []
lfhf_notclose =[]
standard_notclose =[]
for row1 in f_bullet:
    if row1[10] == "1":
        print(row1[8])
        for row2 in f_lfhf:
            dt0 = row1[8] - timedelta(milliseconds=6000)
            dt1 = row1[8] - timedelta(milliseconds=2000)
            dt2 = row1[8] + timedelta(milliseconds=2000)
            if dt0 <= row2[0] <= dt1:
                standard.append(row2)
            elif dt1 <= row2[0] <= dt2:
                lfhf_close.append(row2)
    elif row1[10] == "0":
        print(row1[8])
        for row3 in f_lfhf:
            dt0 = row1[8] - timedelta(milliseconds=6000)
            dt1 = row1[8] - timedelta(milliseconds=2000)
            dt2 = row1[8] + timedelta(milliseconds=2000)
            if dt0 <= row3[0] <= dt1:
                standard_notclose.append(row3)
            elif dt1 <= row3[0] <= dt2:
                lfhf_notclose.append(row3)



with open("./"+name+"/organizedLFHF.csv", 'w', newline = '') as file1:
    writer = csv.writer(file1)
    writer.writerows(lfhf_close)

with open("./"+name+"/stdLFHF.csv", 'w', newline = '') as file2:
    writer = csv.writer(file2)
    writer.writerows(standard)

with open("./"+name+"/notcloseLFHF.csv", 'w', newline = '') as file3:
    writer = csv.writer(file3)
    writer.writerows(lfhf_notclose)

with open("./"+name+"/notclosestdLFHF.csv", 'w', newline = '') as file4:
    writer = csv.writer(file4)
    writer.writerows(standard_notclose)
