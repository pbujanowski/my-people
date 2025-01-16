output "services_images_upload_queue_arn" {
  value = aws_sqs_queue.services_images_upload_queue.arn
}

output "services_images_upload_dead_letter_queue_arn" {
  value = aws_sqs_queue.services_images_upload_dead_letter_queue.arn
}

output "services_images_upload_lambda_arn" {
  value = aws_lambda_function.services_images_upload_lambda.arn
}

output "services_images_bucket_arn" {
  value = aws_s3_bucket.services_images_bucket.arn
}
