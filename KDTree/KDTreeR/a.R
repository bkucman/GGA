x <- c(6,7,8,9,10,11,12,13,14,15,16,17,18,20)
y <- c(22,18,13,24,21,15,25,20,17,12,23,16,29,26)
plot(x,y)

rect(9, 14, 15, 23,
     col=NULL, border=par("fg"), lty=NULL, lwd=par("lwd"), xpd=FALSE)
rect(7, 13, 11, 21,
     col=NULL, border=par("fg"), lty=NULL, lwd=par("lwd"), xpd=FALSE)

x1 <- c(3,3,3,3,3,3,3,3)
y1 <- c(1,2,3,4,5,6,7,8)

x2 <- c(1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23)
y2 <- c(4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26)
plot(x2,y2)
rect(7, 13, 11, 21,
     col=NULL, border=par("fg"), lty=NULL, lwd=par("lwd"), xpd=FALSE)
plot(x1,y1)