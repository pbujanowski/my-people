module "services_images_bucket" {
  source = "./modules/services_images_bucket"
}

module "services_images_upload_lambda" {
  source = "./modules/services_images_upload_lambda"

  sqs_event_source_arn = module.services_images_upload_queue.queue_arn
}

module "services_images_upload_queue" {
  source = "./modules/services_images_upload_queue"
}
