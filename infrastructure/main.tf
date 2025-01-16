# Images S3 bucket.
resource "aws_s3_bucket" "services_images_bucket" {
  bucket = var.services_images_bucket_name
}

# Images S3 bucket ACL.
resource "aws_s3_bucket_acl" "services_images_bucket_acl" {
  bucket = aws_s3_bucket.services_images_bucket.id
  acl = "private"
}

# Images.Upload SQS queue.
resource "aws_sqs_queue" "services_images_upload_queue" {
  name = var.services_images_upload_queue_name
}

# Images.Upload SQS dead-letter queue.
resource "aws_sqs_queue" "services_images_upload_dead_letter_queue" {
  name = var.services_images_upload_dead_letter_queue_name
  redrive_allow_policy = jsonencode({
    redrivePermission = "byQueue",
    sourceQueueArns = [aws_sqs_queue.services_images_upload_queue.arn]
  })
}

resource "aws_sqs_queue_redrive_policy" "services_images_upload_queue_redrive_policy" {
  queue_url = aws_sqs_queue.services_images_upload_queue.id
  redrive_policy = jsonencode({
    deadLetterTargetArn = aws_sqs_queue.services_images_upload_dead_letter_queue.arn,
    maxReceiveCount = 3
  })
}

# IAM role for the Images.Upload Lambda function.
resource "aws_iam_role" "services_images_upload_lambda_role" {
  name = var.services_images_upload_lambda_role_name

  assume_role_policy = jsonencode({
    Version = "2012-10-17"
    Statement = [
      {
        Action = "sts:AssumeRole"
        Principal = {
          Service = "lambda.amazonaws.com"
        }
        Effect = "Allow"
        Sid    = ""
      },
    ]
  })
}

# Policies for the IAM role.
resource "aws_iam_role_policy_attachment" "services_images_upload_lambda_sqs_policy" {
  policy_arn = "arn:aws:iam::aws:policy/service-role/AWSLambdaSQSQueueExecutionRole"
  role       = aws_iam_role.services_images_upload_lambda_role.name
}

resource "aws_iam_role_policy_attachment" "services_images_upload_lambda_s3_policy" {
  policy_arn = "arn:aws:iam::aws:policy/AmazonS3FullAccess"
  role       = aws_iam_role.services_images_upload_lambda_role.name
}

# Images.Upload Lambda function.
resource "aws_lambda_function" "services_images_upload_lambda" {
  function_name = var.services_images_upload_lambda_name
  role = aws_iam_role.services_images_upload_lambda_role.arn
  handler = "MyPeople.Lambdas.Images.Upload.Function::MyPeople.Lambdas.Images.Upload.Function.Function::FunctionHandler"
  runtime = "dotnet8"
  filename = var.services_images_upload_lambda_filename
  timeout = var.services_images_upload_lambda_timeout
}

# Images.Upload SQS event source mapping for the Lambda function.
resource "aws_lambda_event_source_mapping" "services_images_upload_sqs_event_source" {
  event_source_arn = aws_sqs_queue.services_images_upload_queue.arn
  function_name    = aws_lambda_function.services_images_upload_lambda.arn
  enabled          = true
}
