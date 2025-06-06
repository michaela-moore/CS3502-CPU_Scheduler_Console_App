﻿# CS3502-CPU_Scheduler_Console_App
## Description
This project simulates CPU scheduling algorithms to understand process management. The application is designed as a console app that takes user input (a number to randomly generate processes) to run through each scheduling algorithm. It helps users see how different scheduling methods impact wait times, turnaround times, and CPU utilization.

## Features
Implements 4 scheduling algorithms with performance logging
- Highest Response Rate (HRR)
- Shortest Remaining Time First (SRTF)
- Shortest Job First (SJF)
- First Come First Served (FCFS)

# Performance Metrics 
The application computes and measures details about each process and algorithm as it processes a list of processes.

- Average Turnaround Time (TAT)
- Average Wait Time  (WT)
- Average Response Time (Resp. T)
- Total time (in ms)
- CPU Utilization
- Throughput (process/second)
- Busy Time


## Prereqs
- .NET SDK 8.0
- Visual Studio 2022 or compatible IDE

## Running the app locally 

### Clone the repo

`git clone https://github.com/michaela-moore/CS3502-CPU_Scheduler_Console_App.git`

### Open the project folder in an IDE

Update dependencies, run

`dotnet restore`

### Start a build

`dotnet build`

### Run the app

`dotnet run`
  
