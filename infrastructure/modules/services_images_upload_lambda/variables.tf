variable "lambda_role_name" {
  type = string
  description = "Name for 'MyPeople.Lambdas.Images.Upload' lambda role."
  default = "services-images-upload-lambda-role"
}

variable "lambda_name" {
  type = string
  description = "Name for 'MyPeople.Lambdas.Images.Upload' lambda."
  default = "services-images-upload-lambda"
}

variable "lambda_filename" {
  type = string
  description = "Name for 'MyPeople.Lambdas.Images.Upload' lambda file."
  default = "../src/Lambdas/Images/Upload/MyPeople.Lambdas.Images.Upload.Function/bin/Release/net8.0/MyPeople.Lambdas.Images.Upload.Function.zip"
}

variable "lambda_timeout" {
  type = number
  description = "Timeout for 'MyPeople.Lambdas.Images.Upload' lambda."
  default = 30
}

variable "sqs_event_source_arn" {
  type = string
  description = "ARN for 'MyPeople.Lambdas.Images.Upload event source lambda."
}
