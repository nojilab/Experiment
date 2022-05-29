import pandas as pd
import os
import glob

name = input("name:")

os.chdir(name+'/RRI')
df = pd.DataFrame(columns = [])

for i in glob.glob("*FrequencyAnalysisData.csv"):
    tmp = pd.read_csv(i)
    print(tmp)
    df = pd.concat([df, tmp])

print(df)
df.to_csv("LFHF.csv", index = False)