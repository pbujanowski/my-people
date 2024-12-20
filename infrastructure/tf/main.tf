resource "aws_s3_bucket" "services_images_bucket" {
  bucket = var.services_images_bucket_name
}

resource "aws_s3_bucket_acl" "services_images_bucket_acl" {
  bucket = aws_s3_bucket.services_images_bucket.id
  acl = "public-read"
}

resource "aws_s3_bucket_versioning" "services_images_bucket_versioning" {
  bucket = aws_s3_bucket.services_images_bucket.id

  versioning_configuration {
    status = "Enabled"
  }
}
