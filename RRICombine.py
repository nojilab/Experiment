import pandas as pd
import os
import glob

name = input("name:")

os.chdir(name+'/RRI')
df = pd.DataFrame(columns = [])

for i in glob.glob("*FilteredData.csv"):
    tmp = pd.read_csv(i, skiprows=[0,1,2])
    print(tmp)
    df = pd.concat([df, tmp])

print(df)
df.to_csv("RRI.csv", index = False)