
 classifyWithNNt <- function(pathToDirectory, fileName, hiddenNeuronsValue, iterationValue, decayValue, maxNWtsValue)
{
   library(plyr)
   library(rpart)
   library(FSelector)
   library(corrplot)
   library(MASS)
   library(class)
   library(caret)
   library(nnet)
   library(ggplot2)
   
  pkgTest("rpart")
  pkgTest("plyr")
  pkgTest("FSelector")
  pkgTest("corrplot")
  pkgTest("Mass")
  pkgTest("class")
  pkgTest("caret")
  pkgTest("nnet")
  pkgTest("ggplot2")
  
  setwd(pathToDirectory)
  someData <- read.csv2(fileName)
  
  
  k = 10
  
  accuracyNntFilter<- vector('numeric')
  accuracyNnt <- vector('numeric')
  timeNNt <- vector('numeric')
  timeNNtFilter <- vector('numeric')
  
  preprocessing <- function(dataName, classColumn) {
    dataName <-as.data.frame(dataName)
    dataName <-na.omit(dataName)
    dataName[, names(dataName)== classColumn] <- as.factor(dataName[,names(dataName) == classColumn])
    
    x<- as.matrix(dataName[,names(dataName) != classColumn])
    mode(x) = "numeric"
    dataName[,names(dataName) != classColumn] <- x
    
    return((dataName))
  }
  
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
  
  countAccuraciesNnt <- function(class, trainingset, testset, hiddenNeuronsValue, iterationValue, decayValue, maxNWtsValue)
  {
    neuralNetwork <- nnet(class, data = trainingset, size = hiddenNeuronsValue, maxit = iterationValue, decay = decayValue, MaxNWts = maxNWtsValue)
    pred.neuralNetwork <- predict(neuralNetwork,testset,type = "class")
    fold_accuracy <- mean(pred.neuralNetwork == testset$Class)
    return(fold_accuracy)
  }
  
  addColumnWithNumberOfObservation <- function(dataName, k)
  {
    id <- sample(1:k, nrow(dataName), replace = TRUE)
    return(id)
  }
  
  someData$Class <- as.numeric(someData$Class)
  
  someData <- preprocessing(someData, "Class")
  
  someData$id <- addColumnWithNumberOfObservation(someData, k)
  
  namesXlab <- c("nnt", "nnt after filtering")
  
  list <- 1:k
  
  for (i in 1:k) {
      
      trainingset <- subset(someData, id %in% list[-i])
      testset <- subset(someData, id %in% c(i))
      
      trainingset$id <- NULL
      testset$id <- NULL
      
      start <- Sys.time()
      accuracyNnt <- countAccuraciesNnt(Class~., trainingset, testset, hiddenNeuronsValue, iterationValue, decayValue, maxNWtsValue)
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
 
 lol <- function(text)
 {
   return (text)
 }
 