# Images.Upload SQS queue.
resource "aws_sqs_queue" "queue" {
  name = var.queue_name
}

# Images.Upload SQS dead-letter queue.
resource "aws_sqs_queue" "dead_letter_queue" {
  name = var.dead_letter_queue_name
  redrive_allow_policy = jsonencode({
    redrivePermission = "byQueue",
    sourceQueueArns = [aws_sqs_queue.queue.arn]
  })
}

resource "aws_sqs_queue_redrive_policy" "queue_redrive_policy" {
  queue_url = aws_sqs_queue.queue.id
  redrive_policy = jsonencode({
    deadLetterTargetArn = aws_sqs_queue.dead_letter_queue.arn,
    maxReceiveCount = 3
  })
}
