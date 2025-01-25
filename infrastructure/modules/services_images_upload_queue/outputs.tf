output "queue_arn" {
  value = aws_sqs_queue.queue.arn
}

output "dead_letter_queue_arn" {
  value = aws_sqs_queue.dead_letter_queue.arn
}
