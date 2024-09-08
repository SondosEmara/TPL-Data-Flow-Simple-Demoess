
# TPL Data Flow Demo

## Overview

This project demonstrates the use of the Task Parallel Library (TPL) Data Flow in .NET to process data in parallel using blocks and pipelines.

## Features

- **TransformBlock**: Processes input data and produces output data.
- **BufferBlock**: Buffers data for further processing.
- **ActionBlock**: Performs an action on each piece of data received.
- **BroadCastBlock**: Buffers To All Consumers. (Broad Cast Message to all Target Blocks ).
- **TransformManyBlock**: return IEnumerable Toutput
- **Filter**: Filter which soucre Blcok send data to which targetBlock..
- **Demo For Tradinional Way Before TPL**
- **Demo For Tpl Parallel with different Ways (Async Programming/Sync Programming)**

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version X.X or higher)

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/SondosEmara/TPL-Data-Flow.git
   
2. Navigate to the project directory:
   ```bash
   cd TPL-Data-Flow




