from datetime import datetime as dt
from datetime import timedelta
import csv

def Str_to_Time_experiment(f,i):
    l = []
    for row in f:
        if not row == []:
            if not row[2] == "worldtime":
                print(row)
                row[i] = dt.strptime(row[i], '%Y-%m-%d %H:%M:%S.%f')
        l.append(row)
    return l

name = "Sugizaki"
CopPosData = open("./"+name+"/CopPos/CopPosSaveData.csv", "r", encoding="ms932", errors="", newline="" )
f_cp = csv.reader(CopPosData, delimiter=",", doublequote=True, lineterminator="\r\n", quotechar='"', skipinitialspace=True)

# tdatetime = dt.strptime(f, '%Y/%m/%d %H:%M:%S.%f')

header = next(f_cp)
f_cp = Str_to_Time_experiment(f_cp, 2)
CopPos = []
i=1
for row in f_cp:
    if not row == []: 
        if not row[2] == "worldtime":
            t = row[2] + timedelta(seconds=2)
        # if i<97154:
        #     row[2] = "2022-05-04 14:" + row[2]
        # elif i<251919:
        #     row[2] = "2022-05-04 15:" + row[2]
        # else:
        #     row[2] = "2022-05-04 16:" + row[2]
            row[2] = t.strftime('%Y-%m-%d %H:%M:%S.%f')
            CopPos.append(row)
    i=i+1

with open("./"+name+"/CopPos/CopPosData.csv", 'w', newline = '') as file1:
    writer = csv.writer(file1)
    writer.writerows(CopPos)
