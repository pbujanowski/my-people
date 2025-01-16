# Images S3 bucket.
resource "aws_s3_bucket" "bucket" {
  bucket = var.bucket_name
}

# Images S3 bucket ACL.
resource "aws_s3_bucket_acl" "bucket_acl" {
  bucket = aws_s3_bucket.bucket.id
  acl = "private"
}
