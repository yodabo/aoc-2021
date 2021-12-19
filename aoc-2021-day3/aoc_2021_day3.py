import requests
from os.path import exists

year = 2021
day = 1
cacheFile = "c:\\users\\<my username>\\source\\repos\\AOC\\data\\{}\\{}.txt".format(year, day)

if not exists(cacheFile):
  url = "https://adventofcode.com/{}/day/{}/input".format(year, day)
  cookies = dict(session="<my session>")
  r = requests.get(url, cookies=cookies)
  print("making request to " + url);
  print("status: {}".format(r.status_code));
  print("message: {}".format(r.text));
  f = open(cacheFile, "w")
  f.write(r.text)
  f.flush()
  f.close()

#f1 = open(cacheFile)
#lines = f1.readlines();
#f1.close()

levels = list(map(int, open(cacheFile)))
count = lambda step: sum(lhs < rhs for (lhs,rhs) in zip(levels[:-step], levels[step:]))
print(count(1), count(3))




