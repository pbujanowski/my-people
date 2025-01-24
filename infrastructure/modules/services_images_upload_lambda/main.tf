# IAM role for the Images.Upload Lambda function.
resource "aws_iam_role" "lambda_role" {
  name = var.lambda_role_name

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
resource "aws_iam_role_policy_attachment" "lambda_sqs_policy" {
  policy_arn = "arn:aws:iam::aws:policy/service-role/AWSLambdaSQSQueueExecutionRole"
  role       = aws_iam_role.lambda_role.name
}

resource "aws_iam_role_policy_attachment" "lambda_s3_policy" {
  policy_arn = "arn:aws:iam::aws:policy/AmazonS3FullAccess"
  role       = aws_iam_role.lambda_role.name
}

# Images.Upload Lambda function.
resource "aws_lambda_function" "lambda" {
  function_name = var.lambda_name
  role = aws_iam_role.lambda_role.arn
  handler = "MyPeople.Lambdas.Images.Upload.Function::MyPeople.Lambdas.Images.Upload.Function.Function::FunctionHandler"
  runtime = "dotnet8"
  filename = var.lambda_filename
  source_code_hash = filebase64sha256(var.lambda_filename)
  timeout = var.lambda_timeout
}

# Images.Upload SQS event source mapping for the Lambda function.
resource "aws_lambda_event_source_mapping" "sqs_event_source" {
  event_source_arn = var.sqs_event_source_arn
  function_name    = aws_lambda_function.lambda.arn
  enabled          = true
}
