variable "region" {
  type = string
  description = "AWS region."
  default = "eu-central-1"
}

variable "services_images_upload_queue_name" {
  type = string
  description = "Name for 'MyPeople.Lambdas.Images.Upload' lambda queue."
  default = "services-images-upload-queue"
}

variable "services_images_upload_dead_letter_queue_name" {
  type = string
  description = "Name for 'MyPeople.Lambdas.Images.Upload' lambda dead-letter queue."
  default = "services-images-upload-dead-letter-queue"
}

variable "services_images_upload_queue_policy_statement_id" {
  type = string
  description = "ID for 'MyPeople.Lambdas.Images.Upload' lambda queue policy statement."
  default = "services-images-upload-queue-statement"
}

variable "services_images_upload_lambda_role_name" {
  type = string
  description = "Name for 'MyPeople.Lambdas.Images.Upload' lambda role."
  default = "services-images-upload-lambda-role"
}

variable "services_images_upload_lambda_name" {
  type = string
  description = "Name for 'MyPeople.Lambdas.Images.Upload' lambda."
  default = "services-images-upload-lambda"
}

variable "services_images_upload_lambda_filename" {
  type = string
  description = "Name for 'MyPeople.Lambdas.Images.Upload' lambda file."
  default = "../../src/Lambdas/Images/Upload/MyPeople.Lambdas.Images.Upload.Function/bin/Release/net8.0/MyPeople.Lambdas.Images.Upload.Function.zip"
}

variable "services_images_upload_lambda_timeout" {
  type = number
  description = "Timeout for 'MyPeople.Lambdas.Images.Upload' lambda."
  default = 30
}

variable "services_images_bucket_name" {
  type = string
  description = "Name for 'MyPeople.Services.Images' bucket."
  default = "services-images-bucket"
}

variable "services_images_logs_bucket_name" {
  type = string
  description = "Name for 'MyPeople.Services.Images' logs bucket."
  default = "services-images-logs-bucket"
}
