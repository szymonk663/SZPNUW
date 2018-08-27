#!/bin/bash
gcloud beta container --project "szpnuw-project" clusters create "szpnuw-cluster-1" --zone "europe-west3-b" --username "admin" --cluster-version "1.9.7-gke.5" --machine-type "n1-standard-1" --image-type "COS" --disk-type "pd-standard" --disk-size "100" --node-labels apptype=portal --scopes "https://www.googleapis.com/auth/compute","https://www.googleapis.com/auth/devstorage.read_only","https://www.googleapis.com/auth/logging.write","https://www.googleapis.com/auth/monitoring","https://www.googleapis.com/auth/servicecontrol","https://www.googleapis.com/auth/service.management.readonly","https://www.googleapis.com/auth/trace.append" --num-nodes "1" --enable-cloud-logging --enable-cloud-monitoring --network "default" --subnetwork "default" --addons HorizontalPodAutoscaling,HttpLoadBalancing,KubernetesDashboard --no-enable-autoupgrade --enable-autorepair

gcloud container node-pools create nginx-node --cluster "szpnuw-cluster-1" --zone "europe-west3-b" --machine-type "n1-standard-2" --image-type "COS" --disk-type "pd-standard" --disk-size "100" --node-labels apptype=nginx --scopes "https://www.googleapis.com/auth/compute","https://www.googleapis.com/auth/devstorage.read_only","https://www.googleapis.com/auth/logging.write","https://www.googleapis.com/auth/monitoring","https://www.googleapis.com/auth/servicecontrol","https://www.googleapis.com/auth/service.management.readonly","https://www.googleapis.com/auth/trace.append" --num-nodes "1" --no-enable-autoupgrade --enable-autorepair

gcloud container node-pools create database-node --cluster "szpnuw-cluster-1" --zone "europe-west3-b" --machine-type "n1-standard-1" --image-type "COS" --disk-type "pd-standard" --disk-size "100" --node-labels apptype=database --scopes "https://www.googleapis.com/auth/compute","https://www.googleapis.com/auth/devstorage.read_only","https://www.googleapis.com/auth/logging.write","https://www.googleapis.com/auth/monitoring","https://www.googleapis.com/auth/servicecontrol","https://www.googleapis.com/auth/service.management.readonly","https://www.googleapis.com/auth/trace.append" --num-nodes "1" --no-enable-autoupgrade --enable-autorepair

gcloud container node-pools create microservics-nodes --cluster "szpnuw-cluster-1" --zone "europe-west3-b" --machine-type "n1-standard-2" --image-type "COS" --disk-type "pd-standard" --disk-size "100" --node-labels apptype=microservices --scopes "https://www.googleapis.com/auth/compute","https://www.googleapis.com/auth/devstorage.read_only","https://www.googleapis.com/auth/logging.write","https://www.googleapis.com/auth/monitoring","https://www.googleapis.com/auth/servicecontrol","https://www.googleapis.com/auth/service.management.readonly","https://www.googleapis.com/auth/trace.append" --num-nodes "1" --enable-autoscaling --min-nodes "1" --max-nodes "5" --no-enable-autoupgrade --enable-autorepair

gcloud container clusters get-credentials szpnuw-cluster-1 --zone europe-west3-b --project szpnuw-project

kubectl create -f config.yaml
