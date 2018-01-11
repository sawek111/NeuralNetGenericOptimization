VALIDATIONS = 3


pkgTest <- function(x)
{
  if (!require(x,character.only = TRUE))
  {
    install.packages(x,dep=TRUE)
    if(!require(x,character.only = TRUE)) stop("Package not found")
  }
}

createProgressBar <- function(iterationNumber)
{
  progress.bar <- create_progress_bar("text")
  progress.bar$init(iterationNumber)
  return(progress.bar)
}

countAccuraciesNnt <- function(class, trainingset, testset, hiddenNeuronsValue, iterationValue, decayValue)
{
  neuralNetwork <- nnet(class, data = trainingset, size = hiddenNeuronsValue, maxit = iterationValue, decay = decayValue, trace = FALSE, MaxNWts = 1000000)
  pred.neuralNetwork <- predict(neuralNetwork,testset,type = "class")
  fold_accuracy <- mean(pred.neuralNetwork == testset$Class)
  return(fold_accuracy)
}

addColumnWithNumberOfObservation <- function(dataName, k)
{
  id <- sample(1:k, nrow(dataName), replace = TRUE)
  return(id)
} 

preprocessing <- function(dataName, classColumnNumber) {
  dataName <-as.data.frame(dataName)
  dataName <-na.omit(dataName)
  colnames(dataName)[classColumnNumber] <- "Class"
  dataName$Class <- as.factor(dataName$Class)
  
  x<- as.matrix(dataName[,names(dataName) != "Class"])
  mode(x) = "numeric"
  dataName[,names(dataName) != "Class"] <- x
  
  return((dataName))
}

loadPackages <- function()
{
  pkgTest("rpart")
  pkgTest("plyr")
  pkgTest("corrplot")
  pkgTest("MASS")
  pkgTest("class")
  pkgTest("caret")
  pkgTest("nnet")
  pkgTest("ggplot2")
  
  library(plyr)
  library(rpart)
  library(corrplot)
  library(MASS)
  library(class)
  library(caret)
  library(nnet)
  library(ggplot2)
  
  return()
}

test <- function()
{
  results <- c(1.0)
  return (results)
  }

classifyWithNNt <- function(pathToDirectory, classColumnNumber, hiddenNeuronsValue, iterationValue, decayValue)
{
  loadPackages()
  
  someData <- read.csv2(pathToDirectory)
  
  k = VALIDATIONS
  
  accuracyNntFilter<- vector('numeric')
  accuracyNnt <- vector('numeric')
  timeNNt <- vector('numeric')
  timeNNtFilter <- vector('numeric')
  
  someData$Class <- as.numeric(someData$Class)
  
  someData <- preprocessing(someData, classColumnNumber)
  
  someData$id <- addColumnWithNumberOfObservation(someData, k)
  
  namesXlab <- c("nnt", "nnt after filtering")
  
  list <- 1:k
  
  for (i in 1:k) {
      
      trainingset <- subset(someData, id %in% list[-i])
      testset <- subset(someData, id %in% c(i))
      
      trainingset$id <- NULL
      testset$id <- NULL
      
      start <- Sys.time()
      accuracyNnt <- countAccuraciesNnt(Class~., trainingset, testset, hiddenNeuronsValue, iterationValue, decayValue)
      stop <- Sys.time()
      timeNNt <- append(timeNNt, stop-start)
  
    }
  
  
  finalAccuracies <- c(mean(accuracyNnt))
  times <- c(mean(timeNNt))
  
  labelsAcc <- vector()
  labelsTime <- vector()
  
  for (i in 1:length(namesXlab))
  {
    labelsAcc[i] <- round(finalAccuracies[i], 3)
    labelsTime[i] <- round(times[i], 2)
  }
  
  results <- c(finalAccuracies, times)
  
  return(results)
}



#########CALLING
##a <- classifyWithNNt(path,58, 10,100,0.5)
 
 