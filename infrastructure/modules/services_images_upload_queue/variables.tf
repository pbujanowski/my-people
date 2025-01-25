variable "queue_name" {
  type = string
  description = "Name for 'MyPeople.Lambdas.Images.Upload' lambda queue."
  default = "services-images-upload-queue"
}

variable "dead_letter_queue_name" {
  type = string
  description = "Name for 'MyPeople.Lambdas.Images.Upload' lambda dead-letter queue."
  default = "services-images-upload-dead-letter-queue"
}
