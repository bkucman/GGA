write <-function(fileName){
  fileName <-paste( "D:/Inforamtyka/Grafy/OtoczkaWypukla/files/" ,fileName, sep="")
  MyData <- read.csv(file=fileName, header=FALSE, sep=";")
  MyData
  
  plot(MyData$V1,MyData$V2)
  
  polygonData <- read.csv(file="D:/Inforamtyka/Grafy/OtoczkaWypukla/files/polygon.csv", header=FALSE, sep=";")
  polygon(polygonData$V1, polygonData$V2, density = NULL, angle = 45,
              border = NULL, col = NA, lty = par("lty"), fillOddEven = FALSE)
}
args <- commandArgs()
file <- args[2]
write(file)