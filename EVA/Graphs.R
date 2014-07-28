library("ggplot2")

data <- read.csv("Logs/c/Statistics.csv", sep=";")

# Enlever les NaN
data <- data[complete.cases(data),]

# Enelver les valeurs folles
# data <- subset(data, data$Block_Average_Distance < 500)

ggplot(data, aes(x=Step, y=(Cumulative_Average_Distance))) + geom_point()

