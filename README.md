# Enhancing Automotive Cybersecurity with Azure Event Hubs  
_Disaster Recovery and Real-Time Telemetry for Connected Vehicles_

![Static Badge](https://img.shields.io/badge/version-1.0.0-blue.svg)
![Static Badge](https://img.shields.io/badge/License-MIT-yellow.svg)
![Business Continuity and Disaster Recovery - Final Project Report](https://github.com/user-attachments/assets/4ec6eb3f-7f5f-4272-8ee8-af65c2bf4309)

## Contact
Lucas Breno de Souza Noronha Braga

[![Static Badge](https://img.shields.io/badge/WhatsApp-25D366?style=for-the-badge&logo=whatsapp&logoColor=white)](https://api.whatsapp.com/send?phone=12267247739)
[![Static Badge](https://img.shields.io/badge/Microsoft_Outlook-0078D4?style=for-the-badge&logo=microsoft-outlook&logoColor=white)](mailto:lucasbbs@live.fr)
[![Static Badge](https://img.shields.io/badge/GitHub-100000?style=for-the-badge&logo=github&logoColor=white)](https://github.com/lucasbbs/)
[![Static Badge](https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)](https://linkedin.com/in/Lucas-in-Canada/)


## Overview  
This project simulates a resilient, cloud-based telemetry platform for connected vehicles using **Azure Event Hubs with Geo-Disaster Recovery**. It ensures uninterrupted streaming of security-critical data—such as anomaly flags, diagnostic codes, and threat alerts—from in-vehicle systems to cloud analytics, even during regional outages.

## Objectives  
- Enable **real-time event ingestion** from automotive systems.  
- Ensure **high availability** and **automatic failover** using paired Event Hub namespaces.  
- Implement **checkpointing** and **telemetry monitoring** to track processing and recovery.  
- Simulate regional outages and evaluate **RTO** and **event loss**.

## Features  
- 🚗 **In-Vehicle Module Simulation**: Producers generate security events and send them to Azure Event Hubs.  
- ☁️ **Geo-Disaster Recovery**: Alias-based namespace failover without consumer reconfiguration.  
- 📦 **Checkpointing**: Azure Blob Storage integration for event consumption tracking.  
- 📊 **Monitoring & Alerts**: Real-time dashboards with Azure Monitor and Log Analytics.  
- 🧪 **Failover Testing**: Automated tests validate system continuity during simulated outages.

## Tech Stack  
- **Azure Services**: Event Hubs, Blob Storage, Log Analytics, Monitor, Traffic Manager  
- **Language**: C# (.NET)  
- **Tools**: Visual Studio 2022, Azure CLI, ARM Templates  

## Use Case  
This project addresses disaster recovery in the **automotive cybersecurity** domain. It demonstrates how cloud-native tools can meet the high-availability and compliance needs (e.g., ISO/SAE 21434) of modern, connected vehicle systems.

# :closed_book: License

Released in 2025 :closed_book: License

Made with :heart: by [Lucas Breno de Souza Noronha Braga](https://github.com/lucasbbs) 🚀.
This project is under the [MIT license](https://github.com/lucasbbs/iMonitor-Backend/master/LICENSE).
