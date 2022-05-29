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
rriData = open("./"+name+"/RRI/RRI.csv", "r", encoding="ms932", errors="", newline="" )
bulletTime = open("./"+name+"/Bullet/organizedBullet.csv", "r", encoding="ms932", errors="", newline="" )
f_rri = csv.reader(rriData, delimiter=",", doublequote=True, lineterminator="\r\n", quotechar='"', skipinitialspace=True)
f_bullet = csv.reader(bulletTime, delimiter=",", doublequote=True, lineterminator="\r\n", quotechar='"', skipinitialspace=True)

# tdatetime = dt.strptime(f, '%Y/%m/%d %H:%M:%S.%f')

header = next(f_rri) #rri 0:time 1:rri
#header = next(f_bullet) #bullet 8:approachingtime 10:close
f_rri = Str_to_Time_mybeat(f_rri,0)
f_bullet = Str_to_Time_experiment(f_bullet,8)

close=[]
notclose=[]

for row1 in f_bullet:
    if row1[10] == "1":
        print(row1[8])
        standard = []
        rri_close = []
        for row2 in f_rri:
            dt0 = row1[8] - timedelta(milliseconds=6000)
            dt1 = row1[8] - timedelta(milliseconds=2000)
            dt2 = row1[8] + timedelta(milliseconds=2000)
            if dt0 <= row2[0] <= dt1:
                standard.append(row2)
            elif dt1 <= row2[0] <= dt2:
                rri_close.append(row2)
        a = int(rri_close[-1][1])/int(rri_close[0][1])
        b = int(standard[-1][1])/int(standard[0][1])
        close.append([a,b])
    elif row1[10] == "0":
        print(row1[8])
        rri_not_close =[]
        standard_compete =[]
        for row3 in f_rri:
            dt0 = row1[8] - timedelta(milliseconds=6000)
            dt1 = row1[8] - timedelta(milliseconds=2000)
            dt2 = row1[8] + timedelta(milliseconds=2000)
            if dt0 <= row3[0] <= dt1:
                standard_compete.append(row3)
            elif dt1 <= row3[0] <= dt2:
                rri_not_close.append(row3)
        a = int(rri_not_close[-1][1])/int(rri_not_close[0][1])
        b = int(standard_compete[-1][1])/int(standard_compete[0][1])
        notclose.append([a,b])

with open("./"+name+"/RRI2/close.csv", 'w', newline = '') as file1:
    writer = csv.writer(file1)
    writer.writerows(close)

with open("./"+name+"/RRI2/notclose.csv", 'w', newline = '') as file3:
    writer = csv.writer(file3)
    writer.writerows(notclose)